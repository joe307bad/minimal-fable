module Util

let (>.) x f = (fun y -> f y x)

let (/.) x f = f x
let (./) x y = x <| y

let (>>.) f g = fun a s -> f a s /. g

module List =

  let flatten list = List.collect id list

  let flatMap f list = List.map f list /. flatten

  let filterMap (f: 'a -> 's option) (list: 'a list) : 's list =
    list
    /. List.map (fun a ->
      match f a with
      | Some s -> [ s ]
      | None -> []
    )
    /. flatten
