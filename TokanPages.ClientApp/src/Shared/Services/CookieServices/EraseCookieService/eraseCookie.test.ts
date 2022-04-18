/**
 * @jest-environment jsdom
 */
import { GetCookie } from "../GetCookieService/getCookie";
import { EraseCookie } from "./eraseCookie";

 describe("Verify cookies module functionality.", () => 
 {
    let date = new Date();
    const present = date.toUTCString();
 
    it("When EraseCookie. Should return empty string on given cookie value.", () => 
    {
        // Arrange
        Object.defineProperty(window.document, "cookie", 
        {
            writable: true,
            value: `cookieConsent=granted; expires=${present}; path=/; SameSite=Strict;`,
        });

        const expectation = "";

        // Act
        EraseCookie({ cookieName: "cookieConsent" });
        const result = GetCookie({ cookieName: "cookieConsent" });

        // Assert
        expect(result).toBe(expectation);
    });
 });
 