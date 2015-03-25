Entente is an entity class generator for UnityScript. It processes JSON files
describing the desired entity classes and generates objects which are immutable
by default and can be constructed by following a Builder pattern. It also
generates an equivalent C# skeleton for the entities intended for use during
development, since UnityScript types are not visible in IDEs like MonoDevleop.

Here is an example entity description file:

[
  {
    "entityName": "GameListEntry",
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
    "enumName": "ImageType",
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
