# WMP.Alexa
#### A .NET library to intuitively implement Amazon Alexa skills.

## Installation

Search for WMP.Alexa in the NuGet Library, or type `Install-Package WMP.Alexa` into the console.

## Usage

### 1. Extend AlexaSkill

Your skill will be a class extending the abstract class AlexaSkill. You'll need to implement, at the bare minimum, StartSession and EndSession.

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

      [Intent("GetDate")]
      public void GetDate(AlexaSession session, AlexaRequest request, AlexaResponse response)
      {
        // Respond here
      } 
    }

One handler can be invoked by multiple intents by adding more than one Intent attribute. A handler has the job of interpreting the request and then setting the response properly (most easily done using the AlexaResponse helpers below).

### 3. Wire up a controller

The AlexaSkill class presents `HandleRequest(AlexaSession session, AlexaRequest request, AlexaResponse response)`. If you're using .NET WebApi there's a packaged controller that handles request validation per Amazon's guidelines, casts the request and handles the response. Use it like so:

    [Route("exampleskill")]
    public class ExampleSkillController : DefaultAlexaController<ExampleSkill> { }
 
 Note that you'll need to make sure that property names are serialized properly (ie camel-cased). You can do that in your app setup (WebApiConfig.cs in App_Start for a setup from template):
 
    config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
 
You can also validate the request yourself, cast the request, create a response and invoke HandleRequest yourself if you're using something different. In this model the session parameter and the session properties on the request and response parameters are references to the same object.

## Example

The example provided in Examples/ is a simple skill with one intent that reads one date slot and output the day of the week that date was on.

## API

AlexaSession is a Dictionary<string, string>. When a request is received, the session is deserialized to that dictionary; the session parameter and session properties on the request and response objects are all references to the same object. Simply manipulate keys on the dictionary and when the user continues the dialog, the session object will be included in the next request; it is essentially persistent during the entire users dialog and handled all on the other server's end.

The AlexaRequest and AlexaResponse classes follow the format outlined in the [Amazon Alexa custom skill docs](https://developer.amazon.com/public/solutions/alexa/alexa-skills-kit/docs/alexa-skills-kit-interface-reference).

Slots are accessible through a SlotDictionary, which is similar to a "DefaultDictionary", in that you can access keys without worrying whether they are there or not. A slot instance has a Value property, and an IsValid() helper method. This way, the three cases of 1) the request not including the slot, 2) including the slot but leaving the value null and 3) including the slot but leaving the value an empty string can be handled the same way: just check request.Body.Intent.Slots[SLOTNAME].IsValid() and proceed accordingly.

The AlexaResponse class offers the following helper utilities:

#### void SayText(string text)

Sets the output type to text, and sets the output to the text parameter. If called successively, the text parameter is appended to the existing output. Note that you cannot provide both text and SSML output.

#### void SaySSML(string ssml)

Sets the output type to SSML, and sets the output to the ssml parameter. If called successively, ssml is appended to the existing output. Note that you cannot provide both text and SSML output.

#### void RepromptText(string text)

Similar to SayText, except sets the reprompt text.

#### void RepromptSSML(string ssm)

Similar to SaySSML, except sets the reprompt SSML.

#### void ShowCard(string title, string content)

Sets a simple card to be shown, with a title and body text.

#### void ShowCard(string title, string content, string image)

Sets a standard card to be shown, with a title, body text and image via an image URL.

#### void LinkAccount()

Sets the link account card to be shown.

#### void EndSession()

Ends the session, i.e. sets ShouldEndSession to be true.
