import "../../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { UpdatePasswordView } from "./updatePasswordView";

describe("test account group component: updatePasswordView", () => {
    it("should render correctly '<UpdatePasswordView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <UpdatePasswordView
                    isLoading={false}
                    progress={false}
                    caption="Update password"
                    button="Submit"
                    newPassword="user1password"
                    verifyPassword="user1password"
                    keyHandler={jest.fn()}
                    formHandler={jest.fn()}
                    buttonHandler={jest.fn()}
                    disableForm={false}
                    labelNewPassword="New password"
                    labelVerifyPassword="Verify password"
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
