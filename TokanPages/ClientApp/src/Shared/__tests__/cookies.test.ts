import { SetCookie } from "../cookies";

test("Set new cookie string.", () => 
{

    let LDate = new Date();   
    LDate.setTime(LDate.getTime() + (3 * 24 * 60 * 60 * 1000));
    let newExpiry = LDate.toUTCString();
    let expectedValue = `cookieConsent=granted; expires=${newExpiry}; path=/; SameSite=Strict `;

    expect( 
        SetCookie(
        {
            cookieName: "cookieConsent", 
            value: "granted", 
            days: 3,
            sameSite: "Strict",
            secure: "",
            exact: newExpiry
        } 
    )).toBe(expectedValue);

});
