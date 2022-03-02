(* Convert .NET XML Comments file to plain text. *)
open Soup

let soup = read_file "bin/Debug/net6.0/osx-arm64/dotnet-libcpdf.xml" |> parse

let body_contents = soup $$ "member"

let () =
  iter
    (fun node ->
       match element node with
         e ->
           Printf.printf "%s\n\n%s\n\n"
           (R.attribute "name" node)
           (match leaf_text (node $ "summary") with Some s -> s | None -> "")
    )
    body_contents;
