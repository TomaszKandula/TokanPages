import "../../../../setupTests";
import { TextObject } from "Shared/Components/RenderContent/Models";
import { ObjectToText } from "..";

describe("verify ObjectToText method", () => 
{
    it("should return string, when valid JSON object with HTML type provided.", () => 
    {
        // Arrange
        const jsonObject: string = `
        {
            "items": 
            [
                { 
                    "id": "2d844a70-36d1-4cac-a5a2-89fb01b1c52c",
                    "type": "html",
                    "value": "<h1>This is test object</h1><p>We will use it for testing.</p>",
                    "prop": "",
                    "text": "" 
                }
            ]
        }`

        const textObject: TextObject = JSON.parse(jsonObject) as TextObject;
        const input = 
        {
            textObject: textObject
        }

        const expectation: string = "This is test object  We will use it for testing.";

        // Act
        const result = ObjectToText(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("should return empty string, when valid JSON object without HTML type provided.", () => 
    {
        // Arrange
        const jsonObject: string = `
        {
            "items": 
            [
                { 
                    "id": "2d844a70-36d1-4cac-a5a2-89fb01b1c52c",
                    "type": "image",
                    "value": "<img src='http://localhost:5000/wallpaper.jpg' alt='' />",
                    "prop": "",
                    "text": "" 
                }
            ]
        }`

        const textObject: TextObject = JSON.parse(jsonObject) as TextObject;
        const input = 
        {
            textObject: textObject
        }

        // Act
        const result = ObjectToText(input);

        // Assert
        expect(result).toBe("");
    });

    it("should return undefined, when undefined input provided.", () => 
    {
        // Arrange
        const input = 
        {
            textObject: undefined
        }

        // Act
        const result = ObjectToText(input);
        
        // Assert
        expect(result).toBe(undefined);
    });

    it("should return undefined, when empty JSON object provided.", () => 
    {
        // Arrange
        const jsonObject: string = `
        {
            "items": [ ]
        }`

        const textObject: TextObject = JSON.parse(jsonObject) as TextObject;
        const input = 
        {
            textObject: textObject
        }

        // Act
        const result = ObjectToText(input);

        // Assert
        expect(result).toBe(undefined);
    });
});
 