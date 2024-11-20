namespace Components

module Stepper =
  open Feliz
  open Elmish
  open Feliz.UseElmish

  type Model = { ActiveStep: int }

  type Msg = GoToNextStep

  let items = [ 1; 2; 3; 4 ]
  let update msg model =
    match msg with
    | GoToNextStep ->
      {
        model with
            ActiveStep = if model.ActiveStep = items.Length then 1 else model.ActiveStep + 1
      },
      Cmd.none

  let init () = { ActiveStep = 1 }, Cmd.none

  let view = React.functionComponent(fun () ->
    let state, dispatch = React.useElmish(init, update, [| |])
    Html.div [
      Html.nav [
        prop.children (
          items
          |> List.mapi (fun index item ->
            let isLast = index = List.length items - 1

            [
              Html.button [
                prop.className ("circle small " + (if state.ActiveStep = item then "large-elevate" else ""))
                prop.text item
              ]
              if not isLast then
                Html.hr [ prop.className "max" ]
            ])
          |> List.concat
        )
      ]
      Html.div [
        prop.children [
          Html.h1 [ prop.text ("Step " + state.ActiveStep.ToString()) ]
          Html.button [
            prop.className "font-bold"
            prop.children [ Html.i [ prop.text "arrow_back" ]; Html.span [ prop.text "Back" ] ]
          ]
          Html.button [
            prop.className "font-bold"
            prop.children [ Html.span [ prop.text "Next" ]; Html.i [ prop.text "arrow_forward" ] ]
            prop.onClick (fun _ -> dispatch GoToNextStep)
          ]
        ]
      ]
    ]
  )
