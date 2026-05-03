## IMPLEMENTATION_RULES.md

## 1. プロジェクト概要

このプロジェクトは Unity + Naninovel を使用したADVゲームである。

基本サイクルは以下。

- 月曜〜土曜はホーム画面で行動を選ぶ
- 行動には、ショップ、インベントリ、訓練、偵察、金稼ぎ、お休みがある
- スタミナを消費するのは、訓練、偵察、金稼ぎのみ
- お休みを選ぶと次の日へ進む
- 日曜日はホーム画面を挟まず、必ずボス戦へ入る
- ボスHPは週をまたいで持ち越す
- ボスHPが0になったら撃破勝利としてクリアADVへ進む

Naninovel標準機能でできることは標準機能を使う。
標準機能で足りない部分のみ、カスタムコマンド、カスタムUI、新規C#スクリプトで実装する。

---

## 2. 絶対に守るルール

### Naninovelアセット本体を改変しない

Naninovelアセットに含まれる既存スクリプトは改変しない。

追加機能は以下で対応する。

- 新規C#スクリプト
- カスタムコマンド
- カスタムUI
- 自作マネージャー
- .naniシナリオ

### 独自セーブシステムを作らない

進行データは Naninovel Local変数で管理する。

以下は禁止。

- JSON保存
- PlayerPrefsでの進行保存
- ScriptableObjectへの実行時保存
- 独自セーブスロット
- Naninovelとは別の進行保存機構

セーブとロードはNaninovel標準機構に任せる。

### BGDatabaseを直接触らない

バトル、UI、ショップ、訓練、偵察、シナリオ連携スクリプトから、BGDatabaseの生APIを直接呼ばない。

禁止例。

- BGRepo.I
- GetMeta
- FindEntity
- Get<T>
- CodeGen生成クラスの外部直接参照

マスタデータ参照は、必ず自作の窓口クラス経由にする。

例。

- MasterManager
- UnitMasterDB
- ItemDB
- ShopCatalogDB
- BattleCatalogDB
- BattleCheckpointDB

### マスタIDを勝手に作らない

unitId、itemId、battleId、skillId、shopId などはマスタデータ由来の固定IDとする。

表示名や説明文からIDを作らない。
文字列連結でマスタIDを創作しない。

Naninovel変数キーのみ、仕様で決めた形式に従って生成してよい。
ただし、キーに埋め込むIDは必ずマスタ由来の固定IDを使う。

---

## 3. Naninovelの流れ

カスタム機能を呼ぶときは、必ず `@gosub` で専用Entryに隔離する。

例。

```nani
@gosub Shop/ShopEntry
````

Entryファイルでやること。

1. 結果変数 `r_` と一時変数 `tmp_` だけ初期化する
2. 進行変数 `n_` / `b_` / `s_` は初期化しない
3. カスタムコマンドまたはカスタムUIを呼ぶ
4. 終了結果を決め打ちの結果変数に書く
5. `@return` で戻る

Entryで初期化してよい例。

```nani
@set r_shopDone=false
@set r_battleResultCode="None"
@set tmp_selectedUnitId=""
```

Entryで初期化してはいけない例。

```nani
@set n_money=3000
@set n_stamina=10
@set n_wincount=0
@set n_enemyHpPct_Boss1=100
```

---

## 4. 変数ルール

### 進行変数

進行変数はセーブされるデータ。

接頭辞は以下。

* `n_` 数値
* `b_` 真偽値
* `s_` 文字列

例。

```text
n_money
n_stamina
n_week
n_dayOfWeek
n_wincount
n_enemyHpPct_Boss1
b_Recon_Boss1_A_1
```

進行変数は `NewGameInit.nani` でのみ初期化する。
ロード時、通常プレイ中、Entry開始時、UI開始時には初期化しない。

### 結果変数

結果変数は `r_` で始める。

例。

```text
r_shopDone
r_workDone
r_reconDone
r_trainDone
r_battleResultCode
r_battleWinType
r_battleReachedStage
```

結果変数はEntry開始時に初期化してよい。

### 一時変数

一時変数は `tmp_` で始める。

例。

```text
tmp_workGain
tmp_selectedUnitId
tmp_selectedItemId
tmp_enemyHpPct
```

一時変数はEntry開始時に初期化してよい。

---

## 5. New Game初期化

`NewGameInit.nani` は New Game時に一度だけ呼ぶ。

ロード時や通常プレイ中には呼ばない。

初期値。

```nani
@set n_money=3000
@set n_stamina=10
@set n_wincount=0
@set n_dayOfWeek=1
@set n_week=1
@set n_enemyHpPct_Boss1=100
```

偵察フラグも New Game時だけ false にする。

例。

```nani
@set b_Recon_Boss1_A_1=false
@set b_Recon_Boss1_A_2=false
@set b_Recon_Boss1_A_3=false
```

所持ユニット、所持アイテム、ユニットレベルも New Game時だけ初期化する。

---

## 6. シナリオ構成ルール

本筋の流れ。

```text
Main -> DayLoop -> Home -> NextDay / NextWeek
```

機能呼び出しの流れ。

```text
Home -> @gosub 各Entry -> @return -> Home
```

主なファイル。

```text
Title.nani
Main.nani
System/NewGameInit.nani
DayLoop.nani
Home.nani
NextDay.nani
NextWeek.nani
DefeatADV.nani
ClearADV.nani
```

Entryファイル。

```text
Shop/ShopEntry.nani
Recon/ReconEntry.nani
Train/TrainEntry.nani
Work/WorkEntry.nani
Battle/Boss1_BattleEntry.nani
```

---

## 7. 日常ループ

曜日は `n_dayOfWeek` で管理する。

```text
1 = 月曜
2 = 火曜
3 = 水曜
4 = 木曜
5 = 金曜
6 = 土曜
7 = 日曜
```

月曜〜土曜はホームへ行く。
日曜はホームへ行かず、自動でボス戦へ行く。

お休みで次の日に進む。
お休み時、スタミナは10まで回復する。

スタミナは10を超えない。
所持金とスタミナは0未満にならない。

---

## 8. 所持金とスタミナ

所持金。

```text
n_money
```

初期値。

```text
3000
```

ルール。

* 購入成功時のみ減る
* 金稼ぎなどで増える
* 0未満にしない
* 足りない場合は購入不可

スタミナ。

```text
n_stamina
```

初期値。

```text
10
```

ルール。

* 訓練、偵察、金稼ぎで消費する
* お休みで10に戻る
* 10を超えない
* 0未満にしない
* 足りない場合は行動不可

---

## 9. インベントリ変数

stackIdは1から始まる整数。

### ユニット所持数

```text
n_unitCount_<unitId>_<stackId>
```

例。

```text
n_unitCount_Goblin_1
n_unitCount_Goblin_2
```

ルール。

* 1スタック最大1体
* 同じunitIdを複数stackで持てる
* ユニット所持上限は合計30枠

### ユニットレベル

```text
n_unitLv_<unitId>
```

重要。

ユニットレベルはスタック単位ではなく、ユニット種別単位で管理する。

正しい例。

```text
n_unitLv_Goblin
```

間違い。

```text
n_unitLv_Goblin_1
```

`n_unitLv_<unitId>` に stackId を含めてはいけない。

### ユニット編成順

```text
n_unitPartyOrder_<unitId>_<stackId>
```

編成順はstack単位で管理する。

### アイテム所持数

```text
n_itemCount_<itemId>_<stackId>
```

ルール。

* 1スタック最大1個
* 同じitemIdを複数stackで持てる
* 消耗品は使用時に該当stackの数を1減らす
* 非消耗品は使用しても減らさない

---

## 10. 訓練ルール

訓練は `n_unitLv_<unitId>` を上げる機能。

訓練で最終ステータスを保存してはいけない。
保存するのはレベルだけ。

HP計算式。

```text
(基礎値 * 2 + 31) * レベル / 最大レベル + レベル + 10
```

HP以外の計算式。

```text
(基礎値 * 2 + 31) * レベル / 最大レベル + レベル + 5
```

訓練はスタミナを消費する。
スタミナ不足時は実行不可。

---

## 11. ショップルール

ショップは `@gosub Shop/ShopEntry` から呼ぶ。

ShopEntryでは結果変数だけ初期化する。

```nani
@set r_shopDone=false
@shop shopId:Shop_Main
@return
```

ショップで使う主な変数。

```text
n_money
n_wincount
n_unitCount_<unitId>_<stackId>
n_itemCount_<itemId>_<stackId>
```

購入ルール。

* 所持金が足りる場合のみ購入可能
* インベントリに空きstackがある場合のみ購入可能
* 購入成功時のみ `n_money` を減らす
* 購入失敗時は所持金を減らさない
* `n_money` は0未満にしない
* 販売内容は `n_wincount` など進行度で変化してよい

---

## 12. 偵察ルール

偵察は `@gosub Recon/ReconEntry` から呼ぶ。

偵察はスタミナを消費する。
偵察ADVの最後で、対応する偵察フラグをtrueにする。

例。

```nani
@set b_Recon_Boss1_A_3=true
```

偵察フラグ形式。

```text
b_Recon_<boss>_<pattern>_<number>
```

例。

```text
b_Recon_Boss1_A_1
b_Recon_Boss1_A_3
b_Recon_Boss1_A_5
```

偵察フラグは進行変数なので、New Game時以外にfalse初期化してはいけない。

バトルUIは偵察フラグを読み、trueなら敵行動を表示し、falseなら `?` を表示する。

敵行動の表示内容はスクリプトに直書きせず、マスタデータから取得する。

---

## 13. 金稼ぎルール

金稼ぎは `@gosub Work/WorkEntry` から呼ぶ。

最小実装では `.nani` だけでよい。

例。

```nani
@set r_workDone=false
@set tmp_workGain=100
@set n_stamina-=1
@set n_money+=tmp_workGain
@set r_workDone=true
@return
```

`n_wincount` を参照して報酬を変えてよい。
ただし、`n_wincount` を初期化してはいけない。

---

## 14. バトル入口ルール

日曜日のボス戦は以下で呼ぶ。

```nani
@gosub Battle/Boss1_BattleEntry
```

BattleEntry内では以下を呼ぶ。

```nani
@battle battleId:Boss1
```

BattleEntryで初期化してよいもの。

```nani
@set r_battleResultCode="None"
@set r_battleWinType="None"
@set r_battleReachedStage=0
```

BattleEntryで初期化してはいけないもの。

```text
n_enemyHpPct_Boss1
n_wincount
n_week
n_dayOfWeek
```

---

## 15. バトルHP持ち越しルール

敵HPの真実の値はこれ。

```text
n_enemyHpPct_<battleId>
```

Boss1の場合。

```text
n_enemyHpPct_Boss1
```

ルール。

* 0〜100の整数で管理する
* New Game時のみ100に初期化する
* バトル開始時はこの値から始める
* バトル中にHPが変わったらこの値を更新する
* 敗北時は実際の残HPを保存する
* 区切り勝利時はチェックポイント値に補正して保存する
* 撃破勝利時は0を保存する
* `n_wincount` からHPを逆算しない
* `n_wincount` は進行段階であり、HPの真実ではない

---

## 16. ボスチェックポイント

Boss1のチェックポイント。

```text
75
50
25
0
```

`n_wincount` の意味。

```text
0 = まだ未到達
1 = 75%到達済み
2 = 50%到達済み
3 = 25%到達済み
4 = 0%到達、撃破済み
```

フェーズ境界。

```text
Phase1: 75 < HP <= 100
Phase2: 50 < HP <= 75
Phase3: 25 < HP <= 50
Phase4: 0 < HP <= 25
撃破済み: HP <= 0
```

一回のバトルで進むチェックポイントは最大1つ。
次チェックポイントを超えるダメージは切り捨てる。

---

## 17. バトル結果変数

バトル結果はNaninovel側では文字列で扱う。

```text
r_battleResultCode
"None" = 未確定
"Win" = 勝利
"Lose" = 敗北
```

```text
r_battleWinType
"None" = 勝利ではない
"CheckpointWin" = 区切り勝利
"ClearWin" = 撃破勝利
```

```text
r_battleReachedStage
0 = 未到達 / 敗北
1 = 75%到達
2 = 50%到達
3 = 25%到達
4 = 0%到達 / 撃破済み
```

C#内部ではenumを使ってよい。
ただし、Naninovel変数へ書くときは必ず上記の文字列に変換する。

`.nani` 側では数値コードではなく、文字列比較で分岐する。

---

## 18. 戦闘終了判定の優先順位

戦闘終了判定は以下の順で行う。

1. 敵HPが0%以下なら撃破勝利
2. 敵HPが次チェックポイント以下なら区切り勝利
3. プレイヤーライフが0以下なら敗北
4. 6ターン目の強制全滅技でプレイヤーライフを0にして敗北
5. あきらめるボタンで敗北

撃破勝利時は `NextWeek.nani` に行かない。
撃破勝利時は以下を保存する。

```nani
@set n_enemyHpPct_Boss1=0
@set n_wincount=4
```

その後 `ClearADV.nani` へ進む。

区切り勝利時は、到達チェックポイントに応じて `n_wincount` を1〜3に更新する。

敗北時は `n_wincount` を更新しない。

---

## 19. バトル基本ルール

* プレイヤーライフは5
* 1ターンに5アクション
* 敵行動はマスタで決めた固定ローテーション
* ランダム行動は使わない
* 空スロットは許可する
* 空スロットに敵攻撃が来た場合、プレイヤーライフを1減らす
* バトル中にセーブUIは出さない
* あきらめるボタンは強制敗北として扱う

敵行動ローテーション例。

```text
1ターン目: Pattern A
2ターン目: Pattern B
3ターン目: Pattern C
4ターン目: Pattern A
5ターン目: Pattern B
6ターン目: Pattern Z / 強制全滅
```

---

## 20. 実装時の注意

作業前に既存構成を確認する。

既に同等のクラスがある場合、重複クラスを作らない。
無関係な大規模リファクタをしない。
既存の公開クラス名、メソッド名、SerializedField名を勝手に変更しない。

新規追加先の候補。

```text
Assets/Scripts/Game/
Assets/Scripts/Game/Common/
Assets/Scripts/Game/Master/
Assets/Scripts/Game/Inventory/
Assets/Scripts/Game/Shop/
Assets/Scripts/Game/Battle/
Assets/Scripts/Game/NaninovelCommands/
Assets/Scripts/Game/UI/
```

既存プロジェクトに別の命名規則がある場合は、既存規則を優先する。

---

## 21. 実装順の目安

新規実装時は以下の順を優先する。

1. ルールファイル
2. Naninovel変数アクセスサービス
3. 基本.naniループ
4. Home UIの土台
5. 金稼ぎ
6. 訓練
7. 偵察フラグ
8. インベントリ状態
9. ショップ
10. バトルHP持ち越し
11. 最小バトル
12. バトル偵察表示
13. 詳細なバトル効果

基本ループと保存が安定する前に、複雑なバトル効果を実装しない。

---

## 22. 実装後の報告ルール

実装後は必ず以下を報告する。

* 変更したファイル
* Naninovelアセット本体を改変していないか
* BGDatabase生APIを直接使っていないか
* 進行変数をNewGameInit以外で初期化していないか
* Entryで初期化しているのが r_ / tmp_ だけか
* 所持金とスタミナが0未満にならないか
* `n_enemyHpPct_Boss1` を `n_wincount` から復元していないか
* `n_unitLv_<unitId>` に stackId を含めていないか
* 未対応点、TODO、不明点

````

---

## つまり、どこからどこまで貼るか

前回の文章ではなく、**今回の回答の中にある2つのコードブロックだけ**です。

貼る対象はこれです。

```text
AGENTS.md
→ 「# AGENTS.md」から最後まで全部貼る

IMPLEMENTATION_RULES.md
→ 「# IMPLEMENTATION_RULES.md」から最後まで全部貼る
````

元のFIX仕様書は詳細資料として別に残しておき、Codexへ各作業を投げるときに参照させればOKです。今回の `IMPLEMENTATION_RULES.md` は、FIX仕様書の中でも特に事故りやすい **Naninovel改変禁止、Local変数保存、New Game時のみ初期化、BGDatabase直叩き禁止、敵HP持ち越し、ユニットレベルのstackId禁止** を抽出したガードレールです。

[1]: https://developers.openai.com/codex/guides/agents-md?utm_source=chatgpt.com "Custom instructions with AGENTS.md – Codex | OpenAI ..."
