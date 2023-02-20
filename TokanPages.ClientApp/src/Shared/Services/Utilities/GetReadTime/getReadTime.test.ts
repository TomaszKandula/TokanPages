import "../../../../setupTests";
import { IGetReadTime, GetReadTime } from "..";

describe("verify GetReadTime method", () => 
{
    it("should return read time, when number of words and words per minute.", () => 
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
