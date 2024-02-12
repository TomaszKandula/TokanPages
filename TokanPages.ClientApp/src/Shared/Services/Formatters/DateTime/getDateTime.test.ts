import "../../../../setupTests";
import { GetDateTime } from "..";

describe("verify GetDateTime method", () => {
    it("should return 'n/a', when 'n/a' value provided.", () => {
        // Arrange
        const expectation: string = "n/a";
        const input = {
            value: "n/a",
            hasTimeVisible: true,
        };

        // Act
        const result = GetDateTime(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("should return 'n/a', when empty string provided.", () => {
        // Arrange
        const expectation: string = "n/a";
        const input = {
            value: "",
            hasTimeVisible: true,
        };

        // Act
        const result = GetDateTime(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("should return 'n/a', when whitespace provided.", () => {
        // Arrange
        const expectation: string = "n/a";
        const input = {
            value: " ",
            hasTimeVisible: true,
        };

        // Act
        const result = GetDateTime(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("should return formatted date time, when unformatted date and time.", () => {
        // Arrange
        const expectation: string = "01/10/2020, 12:15 PM";
        const input = {
            value: "2020-01-10T12:15:15",
            hasTimeVisible: true,
        };

        // Act
        const result = GetDateTime(input);

        // Assert
        expect(result).toBe(expectation);
    });

    it("should return formatted date only, when unformatted date and time provided.", () => {
        // Arrange
        const expectation: string = "01/10/2020";
        const input = {
            value: "2020-01-10T12:15:15",
            hasTimeVisible: false,
        };

        // Act
        const result = GetDateTime(input);

        // Assert
        expect(result).toBe(expectation);
    });
});
