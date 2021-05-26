import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import SigninForm from "../signinForm";
import { combinedDefaults } from "../../../Redux/combinedDefaults";

describe("Test account component: SigninForm.", () => 
{
    it("Renders correctly '<SigninForm />' when content is loaded.", () => 
    {
        const tree = shallow(<SigninForm signinForm={combinedDefaults.getSigninFormContent} isLoading={false} />);
        expect(tree).toMatchSnapshot();
    });
});
