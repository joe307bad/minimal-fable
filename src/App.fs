open Feliz
open Elmish
open Elmish.React
open Fable.Core.JsInterop

importSideEffects "beercss/dist/cdn/beer.min.css"
importSideEffects "beercss/dist/cdn/beer.min.js"

open Components

type Msg =
  | Increment
  | Decrement

type State = int
let init () = 0

let private button (msg: Msg) (dispatch: Msg -> unit) : ReactElement =
  let text =
    match msg with
    | Increment -> "+"
    | Decrement -> "-"
    + "1"

  Html.button [
    prop.style [
      style.backgroundColor "white"
      style.fontSize (length.em 3)
      style.width (length.em 2)
      style.height (length.em 2)
      style.borderRadius (length.percent 22)
    ]
    prop.text text
    prop.onClick (fun _ -> dispatch msg)
  ]

let update (msg: Msg) (state: State) : State =
  match msg with
  | Increment -> state + 1
  | Decrement -> state - 1

let render (state: State) (dispatch: Msg -> unit) : ReactElement =
  Stepper.view()

Program.mkSimple init update render
|> Program.withReactSynchronous "elmish"
|> Program.run
