// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
// for more guidance on F# programming.

#I @"Z:\Dropbox\PACKT\Testing Applications in F#\book\Appendix - Property Based Testing\code\packages\FsCheck.1.0.4\lib\net45\"
#r "FsCheck"

// hello world
open FsCheck
let property list = list = List.rev (List.rev list)
Check.Quick property

// show all checks
Check.Verbose property

// do 1000 checks
Check.One({ Config.Quick with MaxTest = 1000 }, property)

// failed check
let reverseReverseOfFloatsEqualOriginalList (l : float list) =
    l = List.rev (List.rev l)

Check.Quick reverseReverseOfFloatsEqualOriginalList

// sorting
#I @"Z:\Dropbox\PACKT\Testing Applications in F#\book\Appendix - Property Based Testing\code\packages\NUnit.2.6.4\lib\"
#I @"Z:\Dropbox\PACKT\Testing Applications in F#\book\Appendix - Property Based Testing\code\packages\FsUnit.1.3.0.1\Lib\Net40\"

#r "nunit.framework"
#r "FsUnit.NUnit"

#load "sorting.fs"
open chapter11.sorting
 
// property
let sortingTwiceIsSameAsSortingOnce (list : int list) =
    (sort list) = sort (sort list)

Check.Quick sortingTwiceIsSameAsSortingOnce

let firstItemSmallest (list : int list) =
    (not list.IsEmpty) ==> 
        lazy(List.min list = List.head (sort list))

Check.Quick firstItemSmallest

let lastItemLargest (list : int list) =
    (not list.IsEmpty) ==> 
        lazy(List.max list = List.head (List.rev (sort list)))

Check.Quick lastItemLargest

// lists are of equal length
// all elements in sorted list exists in original list
let permutationOf (list : int list) =
    let exists item = List.exists ((=) item)
    let sorted = sort list

    (sorted.Length = list.Length)
        |@ "same length" .&.
    (sorted |> List.forall (fun x -> list |> (exists x)))
        |@ "all elements exists"

Check.Quick permutationOf

let rec ordered (list : int list) = 
    let rec _ordered = function
    | [] -> true
    | hd :: [] -> true
    | fst :: snd :: tl -> (fst <= snd) && (_ordered (snd :: tl))

    _ordered (sort list)

Check.Quick ordered

type ``sorting a list of numbers`` =
    static member ``once is same as twice`` () = 
        sortingTwiceIsSameAsSortingOnce
    static member ``first item is the smallest`` () = 
        firstItemSmallest
    static member ``last item is the largest`` () = 
        lastItemLargest
    static member ``has same numbers as original`` () = 
        permutationOf
    static member ``is ordered result`` () = 
        ordered

Check.QuickAll typeof<``sorting a list of numbers``>

#load "generators.fs"
open chapter11.generators

let positiveNumberGenerator = Gen.sized <| fun s -> Gen.choose(1, s)

let positiveIntegers1 =
    {new Arbitrary<int>() with 
        override x.Generator = Gen.sized <| fun s -> Gen.choose(1, s)
        override x.Shrinker t = Seq.empty }

let positiveIntegers2 =
    Arb.Default.Int32() 
    |> Arb.mapFilter abs (fun n -> n > 0)

type FizzBuzz =
    // positive integers generator
    static member positiveIntegers =
        Arb.Default.Int32() 
        |> Arb.mapFilter abs (fun n -> n > 0)

    // property
    static member ``fizz when evenly divisible by 3`` d =
        (d % 3 = 0 && d % 5 <> 0) ==> ("fizz" = fizzbuzz d)

    // property
    static member ``buzz when evenly divisible by 5`` d =
        (d % 5 = 0 && d % 3 <> 0) ==> ("buzz" = fizzbuzz d)

    // property
    static member ``fizzbuzz when evenly divisible by 3 and 5`` d =
        (d % 3 = 0 && d % 5 = 0) ==> ("fizzbuzz" = fizzbuzz d)

Arb.registerByType (typeof<FizzBuzz>)
Check.QuickAll (typeof<FizzBuzz>)

Check.All({ Config.Quick with MaxTest = 50 }, (typeof<FizzBuzz>))

#load "brains.fs"
open chapter11.brains

open FsCheck.Commands

let spec =
    let click = 
        { new ICommand<Button,int>() with
            member x.RunActual button = button.Click()
            member x.RunModel value = (value % 4) + 1
            member x.Post (button, value) = value = button.Value |> Prop.ofTestable
            override x.ToString() = "click" }

    { new ISpecification<Button,int> with
      member x.Initial() = (new Button(), 0)
      member x.GenCommand _ = Gen.elements [click] }

Check.Quick (asProperty spec)


     