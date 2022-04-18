/**
 * @jest-environment jsdom
 */

import { ITextObject } from "Shared/Components/ContentRender/Models/textModel";
import { IObjectToText } from "./interface";
import { ObjectToText } from "./objectToText";

describe("Verify helper methods.", () => 
{
    it("Given valid JSON object with HTML type. When TextObjectToRawText. Should return string.", () => 
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

        const textObject: ITextObject = JSON.parse(jsonObject) as ITextObject;
        const input: IObjectToText = 
        {
            textObject: textObject
        }

        const expectation: string = "This is test object  We will use it for testing.";

        // Act
        const result = ObjectToText(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("Given valid JSON object without HTML type. When TextObjectToRawText. Should return empty string.", () => 
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

        const textObject: ITextObject = JSON.parse(jsonObject) as ITextObject;
        const input: IObjectToText = 
        {
            textObject: textObject
        }

        // Act
        const result = ObjectToText(input);

        // Assert
        expect(result).toBe("");
    });

    it("Given undefined input. When TextObjectToRawText. Should return undefined.", () => 
    {
        // Arrange
        const input: IObjectToText = 
        {
            textObject: undefined
        }

        // Act
        const result = ObjectToText(input);
        
        // Assert
        expect(result).toBe(undefined);
    });

    it("Given empty JSON object. When TextObjectToRawText. Should return undefined.", () => 
    {
        // Arrange
        const jsonObject: string = `
        {
            "items": [ ]
        }`

        const textObject: ITextObject = JSON.parse(jsonObject) as ITextObject;
        const input: IObjectToText = 
        {
            textObject: textObject
        }

        // Act
        const result = ObjectToText(input);

        // Assert
        expect(result).toBe(undefined);
    });
});
 