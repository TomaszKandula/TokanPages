/**
 * @jest-environment jsdom
 */
import { GetCookie } from "./getCookie";
import { IGetCookie } from "./interface";

describe("Verify cookies module functionality.", () => 
 {
    let date = new Date();
    const present = date.toUTCString();

    it("When GetCookie. Should return cookie value if such cookie exists.", () => 
    {
        // Arrange
        const expectation = "granted";
        const cookieName: IGetCookie = { cookieName: "cookieConsent" };

        Object.defineProperty(window.document, "cookie", 
        {
            writable: true,
            value: `cookieConsent=${expectation}; expires=${present}; path=/; SameSite=Strict;`,
        });

        // Act
        const result = GetCookie(cookieName);

        // Assert
        expect(result).toBe(expectation);
    });
 
    it("When GetCookie. Should return empty string if cookie does not exists.", () => 
    {
        // Arrange
        const expectation = "";
        const cookieName = { cookieName: "someCookie" };

        Object.defineProperty(window.document, "cookie", 
        {
            writable: true,
            value: `cookieConsent=granted; expires=${present}; path=/; SameSite=Strict;"`,
        });

        // Act
        const result = GetCookie(cookieName);

        // Assert
        expect(result).toBe(expectation);
    });
 });
 