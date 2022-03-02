(* Split pandoc HTML output into chapters *)
open Soup

let soup = read_file "bin/Debug/net6.0/osx-arm64/dotnet-libcpdf.xml" |> parse

let body_contents = soup $$ "doc" |> R.first |> children

let () =
  iter
    (fun node ->
       match element node with
         e -> Printf.printf "%s\n" (to_string node)
    )
    body_contents;
