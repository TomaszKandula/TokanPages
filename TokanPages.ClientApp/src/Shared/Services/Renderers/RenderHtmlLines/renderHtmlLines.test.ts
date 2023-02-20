import "../../../../setupTests";
import { RenderHtmlLinesInput, RenderHtmlLines } from "..";

describe("verify RenderHtmlLines method", () => 
{
    it("should return multiple lines of HTML code, when HTML tag and array of strings provided.", () => 
    {    
        // Arrange
        const expectation: string = "<il>ValueA</il><il>ValueB</il>";
        const input: RenderHtmlLinesInput = 
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
