module Rails

let isNotEmpty x =
    if x="" then raise (System.Exception("Cannot be empty"))
    else if isNull x then raise (System.Exception("Cannot be null"))
    x

let noSpaces (x:string) =
    if x.Contains(" ") then raise (System.Exception("Spaces not allowed"))
    x

let validate (x:string) =
    isNotEmpty x
    |> noSpaces

let useCase (x:string) =
    validate x
    // |> canonicalize
    // |> persist

let bind f x =
    match x with
    | Error e -> Error e
    | Ok x -> f x

let isNotEmpty2 x =
    match x with
    | "" -> Error "Cannot be empty"
    | null -> Error "Cannot be null"
    | _ -> Ok x

let noSpaces2 (x:string) =
    if x.Contains(" ") then Error "Spaces not allowed"
    else Ok x

let validate2 (x:string) =
    isNotEmpty2 x
    |> bind noSpaces2

let useCase2 (x:string) =
    validate2 x
    // |> bind canonicalize
    // |> bind persist