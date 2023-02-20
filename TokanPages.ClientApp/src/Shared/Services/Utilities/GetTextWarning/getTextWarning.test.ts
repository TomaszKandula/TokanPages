import "../../../../setupTests";
import { GetTextWarning } from "..";

describe("verify GetTextWarning method", () => 
{
    it("should return HTML code, when warning object is provided.", () => 
    {
        // Arrange
        const input = {
            object: ["ValueA", "ValueB"],
            template: "<span>Following warning(s) received:</span><ul>{LIST}</ul><span>Please contact IT support.</span>"
        }

        const expectation = "<span>Following warning(s) received:</span><ul><li>ValueA</li><li>ValueB</li></ul><span>Please contact IT support.</span>";

        // Act
        const result = GetTextWarning(input);

        // Assert
        expect(result).toBe(expectation);
    });
});
