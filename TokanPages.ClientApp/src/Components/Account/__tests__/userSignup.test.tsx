import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import UserSignup from "../userSignup";
import { combinedDefaults } from "../../../Redux/combinedDefaults";

describe("Test account group component: userSignup.", () => 
{
    it("Renders correctly '<UserSignup />' when content is loaded.", () => 
    {
        const tree = shallow(<UserSignup content={combinedDefaults.getUserSignupContent.content} isLoading={false} />);
        expect(tree).toMatchSnapshot();
    });
});
