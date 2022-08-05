import "../../../../setupTests";
import { IGetDateTime, GetDateTime } from "..";

describe("Verify GetDateTime.", () => 
{
    it("Given 'n/a' value. When invoke GetDateTime. Should return 'n/a'.", () =>
    {
        // Arrange
        const expectation: string = "n/a";
        const input: IGetDateTime = 
        {
            value: "n/a",
            hasTimeVisible: true
        }

        // Act
        const result = GetDateTime(input);

        // Assert
        expect(result).toBe(expectation);
    });
    
    it("Given empty string. When invoke GetDateTime. Should return 'n/a'.", () =>
    {
        // Arrange
        const expectation: string = "n/a";
        const input: IGetDateTime = 
        {
            value: "",
            hasTimeVisible: true
        }

        // Act
        const result = GetDateTime(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("Given whitespace. When invoke GetDateTime. Should return 'n/a'.", () =>
    {
        // Arrange
        const expectation: string = "n/a";
        const input: IGetDateTime = 
        {
            value: " ",
            hasTimeVisible: true
        }

        // Act
        const result = GetDateTime(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("Given unformatted date and time. When invoke GetDateTime. Should return formatted date time.", () =>
    {
        // Arrange
        const expectation: string = "01/10/2020, 12:15 PM";
        const input: IGetDateTime = 
        {
            value: "2020-01-10T12:15:15",
            hasTimeVisible: true
        }

        // Act
        const result = GetDateTime(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("Given unformatted date and time. When invoke GetDateTime wihtout time. Should return formatted date only.", () =>
    {
        // Arrange
        const expectation: string = "01/10/2020";
        const input: IGetDateTime = 
        {
            value: "2020-01-10T12:15:15",
            hasTimeVisible: false
        }

        // Act
        const result = GetDateTime(input);

        // Assert
        expect(result).toBe(expectation);
    });
});