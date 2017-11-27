(*     
    __  ___                 ________  ________     __ __  ____  ______
   /  |/  /___ _____  _____/ ____/ / / / ____/  __/ // /_/ __ \/ ____/
  / /|_/ / __ `/ __ \/ ___/ /_  / / / / / __   /_  _  __/ / / /___ \  
 / /  / / /_/ / / / / /__/ __/ / /_/ / /_/ /  /_  _  __/ /_/ /___/ /  
/_/  /_/\__,_/_/ /_/\___/_/    \____/\____/    /_//_/  \____/_____/    *)
                                                                      
// Discriminated unions, or, where did my null/guards/validation/polymorphism go?

// Primitives
let myName = "Andy Grant"

// Tuples
let myName = "Andy", "Grant"

// Records
type Fullname = { givenName:string; familyName:string }
let myName = { givenName="Andy"; familyName="Grant" }
type Account = { id:int; email:string; mobile:string }
let myAccount = { id=1; email="john@doe.com"; mobile="07712345678" }
type Account2 = { id:int; contactDetails:obj } // ?

// Discriminated unions
// Used to hold a value that could take on several different, but fixed, types.
// Only one of the types can be in use at any one time
type LandLine = { number:string; ext:string }
type ContactDetails =
    | Email of string
    | Mobile of string
    | LandLine of LandLine

// constructor
let contact = Email "John@doe.com"
let contact = LandLine { number="01611231234"; ext="123" }

// like an abstract class
// alternate approach to polymorphism
type Account3 = { id:int; contactDetails:ContactDetails }

// Pattern matching
let contact x =
    match x with
    | Email email -> printfn "Sending an email to %s" email
    | Mobile mobile -> printfn "Texting %s" mobile
    | LandLine { number=num; ext=ext } -> printfn "Agent calling %s-%s" num ext
// OO = easy to add types, hard to add behaviour
// DU = easy to add behaviour, hard to add types

// Single case DU
type Account4 = { id:int; addressId:int }

// what could go wrong
let doSomethingToAcct x =
    printfn "Does something to account %d" x

type AccountId = AccountId of int
type Account5 = { id:AccountId }

let doSomethingToAcct (AccountId x) =
    printfn "Does something to account %d" x
// a hint of some next level type safety

// consider
let divide x y =
    if y=0 then raise <| System.DivideByZeroException() //guard clause
    x/y
// only positive integers, eg account numbers
// strings of a particular shape, reg exp

// Maybe
// consider
let getAccount x =
    if x=1000 then { id=AccountId 1000 }
    else null

let gotAnAccount { id=AccountId x } =
    printfn "I got account %d" x

// Clever OO devs might say "Null object pattern"
// Patterns fill in for missing language features
// I said no nulls

type Option=
    | Some of Account5
    | None

let getAccount x =
    if x=1000 then Some { id=AccountId 1000 }
    else None

let gotAnAccount { id=AccountId x } =
    printfn "I got account %d" x

type 'a Option=
    | Some of 'a
    | None
// already exists in core libraries
// usually unwrap as close to UI as possible

// DU are low ceremony

// Result
// success and failure
// in OO control flow with exceptions :P
// in JS success and failure callbacks
let divide x y =
    if y<>0 then Ok (x/y)
    else Error "Must be greater than 0"
// int -> int -> int is a lie
// Java tried to fix this problem with declared exceptions
// terrible idea
// C# blows up in your face without warning
// bizarrely, this is somehow less painful

// Bind?