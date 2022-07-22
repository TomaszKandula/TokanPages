import "../../../../setupTests";
import { IGetTextWarning, GetTextWarning } from "..";

describe("Verify GetTextWarning.", () => 
{
    it("Given warning object. When invoke GetTextWarning. Should return HTML code.", () => 
    {
        // Arrange
        const input: IGetTextWarning = 
        {
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
