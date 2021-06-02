import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import SigninFormView from "../signinFormView";
import { combinedDefaults } from "../../../Redux/combinedDefaults";

describe("Test account component: SigninForm.", () => 
{
    it("Renders correctly '<SigninFormView />' when content is loaded.", () => 
    {
        const tree = shallow(<SigninFormView content={combinedDefaults.getSigninFormContent.content} isLoading={false} />);
        expect(tree).toMatchSnapshot();
    });
});
