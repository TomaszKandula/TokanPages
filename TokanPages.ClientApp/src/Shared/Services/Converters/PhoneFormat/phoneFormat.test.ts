import "../../../../setupTests";
import { formatPhoneNumber } from "./phoneFormat";

describe("verify 'formatPhoneNumber' method", () => {
    it("should return null, when empty phone number is provided.", () => {
        // Arrange
        // Act
        const result = formatPhoneNumber("");

        // Assert
        expect(result).toBe(null);
    });

    it("should return formatted phone number, when only phone number is provided.", () => {
        // Arrange
        // Act
        const result = formatPhoneNumber("123000876");

        // Assert
        expect(result).toBe("123 000 876");
    });

    it("should return formatted phone number, when phone number w/2-digit area code is provided.", () => {
        // Arrange
        // Act
        const result = formatPhoneNumber("11222333444");

        // Assert
        expect(result).toBe("(11) 222 333 444");
    });

    it("should return formatted phone number, when phone number w/3-digit area code is provided.", () => {
        // Arrange
        // Act
        const result = formatPhoneNumber("111222333444");

        // Assert
        expect(result).toBe("(111) 222 333 444");
    });
});
