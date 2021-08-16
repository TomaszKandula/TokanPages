import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import SignupFormView from "../userSignupView";
import { combinedDefaults } from "../../../Redux/combinedDefaults";

describe("Test account component: SignupForm.", () => 
{
    it("Renders correctly '<SignupFormView />' when content is loaded.", () => 
    {
        const tree = shallow(<SignupFormView content={combinedDefaults.getSignupFormContent.content} isLoading={false} />);
        expect(tree).toMatchSnapshot();
    });
});
