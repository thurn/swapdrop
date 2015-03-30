Entente is an entity class generator for JavaScript and C#. It processes JSON
files describing the desired entity classes and generates objects which are
immutable by default and can be constructed by following a Builder pattern. It
generates a C# version and a Javascript version of each entity with an
identical API. In this way, code can be written in either language to interact
with the entities.

Here is an example entity description file:

[
  {
    "type": "entity",
    "name": "GameListEntry",
    "desc": "A description of a game in progress.",
    "fields": [
      {
        "name": "vsString",
        "type": "String",
        "desc": "A string describing the players in the game."
      },
      {
        "fieldName": "imageString",
        "type": "ImageString",
        "repeated": true,
        "desc": "An image associated with this game list entry."
      }
    ]
  },
  {
    "type": "enum",
    "name": "ImageType",
    "desc": "Represents the possible types of images in the app."
    "values": [
      {
        "name": "LOCAL",
        "desc": "An image which is included with the application."
      },
      {
        "name": "FACEBOOK",
        "desc": "An image which needs to be downloaded from Facebook."
      }
    ]
  }
]

Here is the generated code for the above description:
