import "../../../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { UserPasswordView } from "./userPasswordView";

describe("test account group component: userPasswordView", () => {
    it("should render correctly '<UserPasswordView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <UserPasswordView
                    isLoading={false}
                    isMobile={false}
                    oldPassword="Provide old password"
                    newPassword="Provide new password"
                    confirmPassword="Confirm new password"
                    keyHandler={jest.fn()}
                    formProgress={false}
                    formHandler={jest.fn()}
                    buttonHandler={jest.fn()}
                    sectionAccountPassword={{
                        caption: "Change Password",
                        labelOldPassword: "123456987",
                        labelNewPassword: "ester_exposito_2020",
                        labelConfirmPassword: "ester_exposito_2020",
                        updateButtonText: "Update",
                    }}
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
