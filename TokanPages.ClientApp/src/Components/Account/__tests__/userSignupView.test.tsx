import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import UserSignupView from "../userSignupView";
import { combinedDefaults } from "../../../Redux/combinedDefaults";

describe("Test account component: SignupForm.", () => 
{
    it("Renders correctly '<UserSignupView />' when content is loaded.", () => 
    {
        const tree = shallow(<UserSignupView content={combinedDefaults.getUserSignupContent.content} isLoading={false} />);
        expect(tree).toMatchSnapshot();
    });
});
