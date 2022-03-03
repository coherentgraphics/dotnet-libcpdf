(* Convert .NET XML Comments file to plain text. *)
open Soup

let soup = read_file "bin/Debug/net6.0/osx-arm64/dotnet-libcpdf.xml" |> parse

let body_contents = soup $$ "member"

let explode s =
  let l = ref [] in
    for p = String.length s downto 1 do
      l := String.unsafe_get s (p - 1)::!l
    done;
    !l

let implode l =
  let s = Bytes.create (List.length l) in
    let rec list_loop x = function
       [] -> ()
     | i::t -> Bytes.unsafe_set s x i; list_loop (x + 1) t
    in
      list_loop 0 l;
      Bytes.to_string s

let rec dropwhile p = function
  | [] -> []
  | h::t -> if p h then dropwhile p t else (h::t)

let string_replace_all x x' s =
  if x = "" then s else
    let p = ref 0
    and slen = String.length s
    and xlen = String.length x in
      let output = Buffer.create (slen * 2) in
        while !p < slen do
          try
            if String.sub s !p xlen = x
              then (Buffer.add_string output x'; p := !p + xlen)
              else (Buffer.add_char output s.[!p]; incr p)
          with
            _ -> Buffer.add_char output s.[!p]; incr p
        done;
        Buffer.contents output

let replacements =
  [("CoherentGraphics.", "");
   ("F:", "");
   ("T:", "");
   ("M:", "");
   ("System.Collections.Generic.", "");
   ("System.", "")]

(* Simple search and replace *)
let replace s =
  let s = ref s in
    List.iter
      (fun (f, t) -> s := string_replace_all f t !s)
      replacements;
    !s

(* Add spaces after commas *)
let rec bulk_comments = function
  | ','::x::t when x <> ' ' -> ','::' '::x::bulk_comments t
  | h::t -> h::bulk_comments t
  | [] -> []

(* Remove spaces at begining of lines *)
let rec remove_spaces = function
  | '\n'::t -> '\n'::remove_spaces (dropwhile (( = ) ' ') t)
  | h::t -> h::remove_spaces t
  | [] -> []

let process s =
  let s = replace s in
  let s = explode s in
  let s = bulk_comments s in
  let s = remove_spaces s in
    implode s

let b = Buffer.create 4096

let () =
  iter
    (fun node ->
       match element node with
         e ->
           Buffer.add_string b
             (Printf.sprintf "%s\n\n%s\n\n"
               (R.attribute "name" node)
               (match leaf_text (node $ "summary") with Some s -> s | None -> ""))
    )
    body_contents;
  print_string (process (Buffer.contents b))
