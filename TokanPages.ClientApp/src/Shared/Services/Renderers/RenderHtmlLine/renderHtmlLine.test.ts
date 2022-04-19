/**
 * @jest-environment jsdom
 */
import { IRenderHtmlLine } from "./interface";
import { RenderHtmlLine } from "./renderHtmlLine";

describe("Verify RenderHtmlLine.", () => 
{ 
    it("Given HTML tag. When invoke RenderHtmlLine. Should render HTML line.", () => 
    {
        // Arrange
        const expectation: string = "<li>This is test item</li>";
        const input: IRenderHtmlLine = 
        {
            tag: "li",
            text: "This is test item"
        }

        // Act
        const result = RenderHtmlLine(input);

        // Assert
        expect(result).toBe(expectation);
    });
 
    it("Given HTML tag and undefined text. When invoke RenderHtmlLine. Should return whitespace.", () => 
    {
        // Arrange
        const expectation = " ";
        const input: IRenderHtmlLine = 
        {
            tag: "li",
            text: undefined
        }

        // Act
        const result = RenderHtmlLine(input);

        // Assert
        expect(result).toBe(expectation);
    });
});
 