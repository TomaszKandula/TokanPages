import "../../../../setupTests";
import { RenderHtmlLine } from "..";

describe("verify RenderHtmlLine method", () => 
{ 
    it("should render HTML line, when HTML tag provided.", () => 
    {
        // Arrange
        const expectation: string = "<li>This is test item</li>";
        const input = 
        {
            tag: "li",
            text: "This is test item"
        }

        // Act
        const result = RenderHtmlLine(input);

        // Assert
        expect(result).toBe(expectation);
    });
 
    it("should return whitespace, when HTML tag have undefined text.", () => 
    {
        // Arrange
        const expectation = " ";
        const input = 
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
 