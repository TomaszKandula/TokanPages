import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { UserSignupView } from "./userSignupView";

describe("test account group component: userSignupView", () => {
    it("should render correctly '<UserSignupView />' when content is loaded.", () => {
        const tree = shallow(
            <UserSignupView
                isLoading={false}
                caption="Create a new account"
                warning="Please use..."
                consent="I agree to the terms of use and privacy policy."
                button="Sign up"
                link="Already have an account? Sign in"
                buttonHandler={jest.fn()}
                keyHandler={jest.fn()}
                formHandler={jest.fn()}
                progress={false}
                firstName="Ester"
                lastName="Exposito"
                email="ester.exposito@gmail.com"
                password="madrilena123"
                terms={false}
                labelFirstName="First name"
                labelLastName="Last name"
                labelEmail="Email address"
                labelPassword="Password"
            />
        );

        expect(tree).toMatchSnapshot();
    });
});
