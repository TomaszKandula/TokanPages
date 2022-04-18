/**
 * @jest-environment jsdom
 */
import { GetTextWarning } from "./getTextWarning";
import { IGetTextWarning } from "./interface";

describe("Verify helper methods.", () => 
{
    it("Given warning object. When GetTextWarning. Should return HTML code.", () => 
    {
        // Arrange
        const input: IGetTextWarning = 
        {
            object: ["ValueA", "ValueB"],
            template: "<span>Following warning(s) received:</span><ul>{LIST}</ul><span>Please contact IT support.</span>"
        }

        const expectation = "<span>Following warning(s) received:</span><ul><li></li>ValueA<li>ValueB</li></ul><span>Please contact IT support.</span>";

        // Act
        const result = GetTextWarning(input);

        // Assert
        expect(result).toBe(expectation);
    });
});
