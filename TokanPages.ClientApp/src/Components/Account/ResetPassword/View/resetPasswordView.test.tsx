import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { ResetPasswordView } from "./resetPasswordView";
import { RESET_FORM } from "../../../../Shared/constants";

describe("test account group component: resetPasswordView", () => {
    it("should render correctly '<ResetPasswordView />' when content is loaded.", () => {
        const tree = shallow(
            <ResetPasswordView
                isLoading={false}
                progress={false}
                caption={RESET_FORM}
                button="Reset"
                email="tokan@dfds.com"
                keyHandler={jest.fn()}
                formHandler={jest.fn()}
                buttonHandler={jest.fn()}
                labelEmail="Email address"
            />
        );

        expect(tree).toMatchSnapshot();
    });
});
