import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import SignupForm from "../signupForm";
import { combinedDefaults } from "../../../Redux/combinedDefaults";

describe("Test account component: SignupForm.", () => 
{
    it("Renders correctly '<SignupForm />' when content is loaded.", () => 
    {
        const tree = shallow(<SignupForm signupForm={combinedDefaults.getSignupFormContent} isLoading={false} />);
        expect(tree).toMatchSnapshot();
    });
});
