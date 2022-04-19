/**
 * @jest-environment jsdom
 */
import { IRenderHtmlLines } from "./interface";
import { RenderHtmlLines } from "./RenderHtmlLines";

describe("Verify RenderHtmlLines.", () => 
{
    it("Given HTML tag and array of strings. When invoke RenderHtmlLines. Should return multiple lines of HTML code.", () => 
    {    
        // Arrange
        const expectation: string = "<il>ValueA</il><il>ValueB</il>";
        const input: IRenderHtmlLines = 
        {
            inputArray: ["ValueA", "ValueB"],
            tag: "il"
        }

        // Act
        const result = RenderHtmlLines(input);

        // Assert
        expect(result).toBe(expectation);
    });
});
