/**
 * @jest-environment jsdom
 */
import { GetReadTime } from "./getReadTime";
import { IGetReadTime } from "./interface";

describe("Verify helper methods.", () => 
{
    it("Given number of words and words per minute. When GetReadTime. Should return read time.", () => 
    {
        // Arrange
        const expectation: string = "5.38";
         const input: IGetReadTime = 
        {
            countWords: 700,
            wordsPerMinute: 130
        }

        // Act
        const result = GetReadTime(input);

        // Assert
        expect(result).toBe(expectation);
    });
});
