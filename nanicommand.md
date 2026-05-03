Naninovel コマンドリファレンス
前提ルール

1コマンド = 1ブロック（目的/最小例/組み合わせ/注意）で記載する。

パラメータのキー指定は省略し、値のみを記述する。

ブロック構造（@beginIf / @while / @group / @pickRandom）は末尾のテンプレートを参照する。

@beginIf

目的：条件分岐を開始する。

最小例：@beginIf condA

組み合わせ：@else @endIf @set

注意：なし

@else

目的：条件分岐の代替ブロックを開始する。

最小例：@else

組み合わせ：@beginIf @endIf @set

注意：なし

@endIf

目的：条件分岐ブロックを終了する。

最小例：@endIf

組み合わせ：@beginIf @else @while

注意：なし

@while

目的：条件付きループを開始する。

最小例：@while loopCond

組み合わせ：@delay @set @endIf

注意：なし

@goto

目的：指定先へ遷移する。

最小例：@goto Main.Start

組み合わせ：@set @wait @stop

注意：なし

@gosub

目的：サブルーチン呼び出しを行う。

最小例：@gosub Side.SceneA

組み合わせ：@return @set @wait

注意：なし

@return

目的：サブルーチンから復帰する。

最小例：@return

組み合わせ：@gosub @set @wait

注意：なし

@wait

目的：待機を入れる。

最小例：@wait input

組み合わせ：@print @voice @bgm

注意：なし

@delay

目的：秒数指定の遅延を入れる。

最小例：@delay 0.5

組み合わせ：@print @sfx @animate

注意：なし

@stop

目的：進行を停止する。

最小例：@stop

組み合わせ：@goto @set @wait

注意：なし

@skip

目的：スキップ状態を切り替える。

最小例：@skip true

組み合わせ：@wait @print @bgm

注意：なし

@await

目的：待機ポイントとして使う。

最小例：@await

組み合わせ：@print @voice @wait

注意：なし

@group

目的：コマンド群のまとまりを作る。

最小例：@group

組み合わせ：@pickRandom @beginIf @while

注意：ブロック構造の例は末尾テンプレ参照

@pickRandom

目的：ランダム選択ブロックを使う。

最小例：@pickRandom 1

組み合わせ：@group @goto @set

注意：ブロック構造の例は末尾テンプレ参照

@i

目的：入力待ちを入れる。

最小例：@i

組み合わせ：@print @voice @wait

注意：なし

@set

目的：式で値を設定する。

最小例：@set score=1

組み合わせ：@beginIf @while @goto

注意：なし

@input

目的：入力で値を受け取る。

最小例：@input playerName string Name

組み合わせ：@set @print @beginIf

注意：なし

@resetState

目的：状態リセットを行う。

最小例：@resetState

組み合わせ：@goto @save @purgeRollback

注意：なし

@purgeRollback

目的：ロールバック情報を整理する。

最小例：@purgeRollback

組み合わせ：@resetState @save @goto

注意：なし

@save

目的：保存を実行する。

最小例：@save

組み合わせ：@set @goto @resetState

注意：なし

@print

目的：テキストを表示する。

最小例：@print Hello

組み合わせ：@append @i @voice

注意：なし

@append

目的：既存テキストに追記する。

最小例：@append ...

組み合わせ：@print @i @wait

注意：なし

@show

目的：Actorを表示する。

最小例：@show hero

組み合わせ：@hide @animate @slide

注意：なし

@hide

目的：Actorを非表示にする。

最小例：@hide hero

組み合わせ：@show @delay @wait

注意：なし

@hideAll

目的：全Actorを非表示にする。

最小例：@hideAll

組み合わせ：@show @delay @bgm

注意：なし

@hideChars

目的：キャラクターを非表示にする。

最小例：@hideChars

組み合わせ：@show @animate @wait

注意：なし

@animate

目的：Actor表示状態を変更する。

最小例：@animate hero 0.3

組み合わせ：@show @slide @wait

注意：なし

@slide

目的：Actorをスライド移動させる。

最小例：@slide hero.happy 0.4

組み合わせ：@show @animate @wait

注意：なし

@bgm

目的：BGM再生/設定を行う。

最小例：@bgm bgm/main

組み合わせ：@sfx @wait @print

注意：なし

@sfx

目的：SFX再生/設定を行う。

最小例：@sfx se/click

組み合わせ：@print @delay @i

注意：なし

@sfxFast

目的：SFXを即時再生する。

最小例：@sfxFast se/tap

組み合わせ：@print @append @delay

注意：なし

@voice

目的：ボイス再生を行う。

最小例：@voice voice/line001

組み合わせ：@print @i @wait

注意：なし

@spawn

目的：オブジェクトを生成する。

最小例：@spawn Effects/Rain

組み合わせ：@despawn @wait @delay

注意：なし

@despawn

目的：生成オブジェクトを破棄する。

最小例：@despawn Effects/Rain

組み合わせ：@spawn @wait @delay

注意：なし

@showUI

目的：指定UIを表示する。

最小例：@showUI TipsUI 0.2 true

組み合わせ：@hideUI @print @i

注意：なし

@hideUI

目的：指定UIを非表示にする。

最小例：@hideUI TipsUI true 0.2 true

組み合わせ：@showUI @wait @print

注意：なし

@camera

目的：カメラ状態をまとめて調整する。

最小例：@camera 0,0,0 0 0,0,0 1 false 0.3 true

組み合わせ：@look @transitionScene @show

注意：なし

@look

目的：視線追従の有効化と範囲設定を行う。

最小例：@look true 0,0,1,1 1,1 true

組み合わせ：@camera @show @animate

注意：なし

@transitionScene

目的：シーン遷移演出を実行する。

最小例：@transitionScene Crossfade 0.5

組み合わせ：@loadScene @unloadScene @wait

注意：なし

@loadScene

目的：シーンを読み込む。

最小例：@loadScene Stage01 true

組み合わせ：@transitionScene @unloadScene @camera

注意：なし

@unloadScene

目的：シーンをアンロードする。

最小例：@unloadScene Stage01

組み合わせ：@loadScene @transitionScene @wait

注意：なし

@addChoice

目的：選択肢を追加する。

最小例：@addChoice 選択肢A ChoiceHandler

組み合わせ：@clearChoice @goto @gosub

注意：なし

@clearChoice

目的：選択肢表示をクリアする。

最小例：@clearChoice ChoiceHandler true

組み合わせ：@addChoice @print @wait

注意：なし

@processInput

目的：入力処理の有効/無効を制御する。

最小例：@processInput true

組み合わせ：@wait @i @skip

注意：なし

@toast

目的：トーストUIで短い通知を表示する。

最小例：@toast 保存しました default 1.5

組み合わせ：@save @set @print

注意：なし

@clearBacklog

目的：バックログ表示内容をクリアする。

最小例：@clearBacklog

組み合わせ：@print @append @wait

注意：なし

@exitToTitle

目的：タイトル遷移を実行する。

最小例：@exitToTitle

組み合わせ：@save @resetState @stop

注意：なし

@lock

目的：ID指定でロック状態を設定する。

最小例：@lock CG001

組み合わせ：@unlock @set @goto

注意：なし

@unlock

目的：ID指定でアンロックする。

最小例：@unlock CG001

組み合わせ：@lock @set @save

注意：なし

@movie

目的：ムービー再生を行う。

最小例：@movie OpMovie 0.3 true true

組み合わせ：@bgm @wait @transitionScene

注意：なし

@openURL

目的：URLを開く。

最小例：@openURL example.com _blank

組み合わせ：@showUI @hideUI @toast

注意：なし

@arrange

目的：キャラクター配置を一括調整する。

最小例：@arrange left,hero right,friend 0.4 true

組み合わせ：@show @hide @animate

注意：なし

@remove

目的：指定Actorを管理対象から外す。

最小例：@remove hero,friend

組み合わせ：@show @hide @arrange

注意：なし

@despawnAll

目的：生成済みオブジェクトを全破棄する。

最小例：@despawnAll true

組み合わせ：@spawn @despawn @wait

注意：なし

@spawnEffect

目的：エフェクト生成を行う（簡易）。

最小例：@spawnEffect true

組み合わせ：@spawn @despawnAll @camera

注意：なし

@<

目的：表示テキストのパラメータを設定する。

最小例：@< Dialogue hero 1

組み合わせ：@print @append @format

注意：なし

@format

目的：テキストテンプレートを設定する。

最小例：@format normal,bold Dialogue

組み合わせ：@print @append @<

注意：なし

@printerCommand

目的：プリンター系コマンドの基底名。

最小例：@printerCommand

組み合わせ：@print @append @format

注意：利用頻度は低め

@audioCommand

目的：オーディオ系コマンドの基底名。

最小例：@audioCommand

組み合わせ：@bgm @sfx @voice

注意：利用頻度は低め

@lipSync

目的：キャラクターのリップシンク許可状態を設定する。

最小例：@lipSync hero,true

組み合わせ：@voice @print @show

注意：なし

@scriptPlaylist

目的：スクリプト再生リスト情報を扱う。

最小例：@scriptPlaylist Main 10

組み合わせ：@goto @gosub @return

注意：なし

ブロック構造テンプレート
if

コード スニペット
@beginIf cond
（ここにコマンド）
@else
（ここにコマンド）
@endIf
while

コード スニペット
@while cond
（ここにコマンド）
@endIf
group + pickRandom

コード スニペット
@group
@pickRandom 1,2
（ここにコマンド）
@else
（ここにコマンド）
@endIf
索引（@コマンド → 目的）
分岐/ループ： @beginIf @else @endIf @while

遷移： @goto @gosub @return

待機： @wait @delay @i @await

制御： @stop @skip @processInput

変数/状態： @set @input @resetState @purgeRollback @save

テキスト： @print @append @< @format

Actor： @show @hide @hideAll @hideChars @animate @slide @arrange @remove

Audio： @bgm @sfx @sfxFast @voice

Spawn： @spawn @despawn @despawnAll @spawnEffect

UI/演出： @showUI @hideUI @toast @camera @look @transitionScene @loadScene @unloadScene @movie @openURL

その他： @lock @unlock @clearBacklog @exitToTitle @scriptPlaylist