import "../../../../setupTests";
import React from "react";
import { render } from "enzyme";
import { UpdatePasswordView } from "./updatePasswordView";

describe("test account group component: updatePasswordView", () => {
    it("should render correctly '<UpdatePasswordView />' when content is loaded.", () => {
        const html = render(
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
        );

        expect(html).toMatchSnapshot();
    });
});
