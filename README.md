# WMP.Alexa
#### A .NET library to intuitively implement Amazon Alexa skills.

## Installation

Search for WMP.Alexa in the NuGet Library, or type `Install-Package WMP.Alexa` into the console.

## Usage

### 1. Extend AlexaSkill

Your skill is will be a class extending the abstract class AlexaSkill. You'll need to implement, at the bare minimum, StartSession and EndSession.

    public class ExampleSkill : AlexaSkill
    {
      // Invoked when the user invokes a skill with no intent.
      public override void StartSession(AlexaSession session, AlexaRequest request, AlexaResponse response)
      {
        response.SayText("Welcome!");
      }

      // Invoked when the session is ended by the user or Alexa.
      public override void EndSession(AlexaSession session, AlexaRequest request, AlexaResponse response)
      {
        // Clean up if needed
      }
    }
    
### 2. Implement and add attributes for intents

Each handler has the same signature; APIs for the AlexaSession, AlexaRequest and AlexaResponse classes are outlined below. Each handler needs to be annotated with the `Intent` attribute as such.

    public class ExampleSkill : AlexaSkill
    {
      /* ... */

      [Intent("GetCar")]
      public void GetCar(AlexaSession session, AlexaRequest request, AlexaResponse response)
      {
        // Respond here
      } 
    }

One handler can be invoked by multiple intents by adding more than one Intent attribute.

### 3. Wire up a controller

The AlexaSkill class presents `HandleRequest(AlexaSession session, AlexaRequest request, AlexaResponse response)`. If you're using .NET WebApi there's a packaged controller that handles request validation per Amazon's guidelines, casts the request and handles the response. Use it like so:

    [Route("exampleskill")]
    public class ExampleSkillController : DefaultAlexaController<ExampleSkill> { }
    
You can also validate the request yourself, cast the request, create a response and invoke HandleRequest yourself if you're using something different. In this model the session parameter and the session properties on the request and response parameters are references to the same object.

## API

AlexaSession is a Dictionary<string, string>. When a request is received, the session is deserialized to that dictionary; the session parameter and session properties on the request and response objects are all references to the same object. Simply manipulate keys on the dictionary and when the user continues the dialog, the session object will be included in the next request; it is essentially persistent during the entire users dialog and handled all on the other server's end.

The AlexaRequest and AlexaResponse classes follow the format outlined in the [Amazon Alexa custom skill docs](https://developer.amazon.com/public/solutions/alexa/alexa-skills-kit/docs/alexa-skills-kit-interface-reference).

The AlexaResponse class offers the following helper utilities:

#### void SayText(string text)

Adds to the output text of the request.
