import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import SignupForm from "../signupForm";
import { signupFormDefault } from "../../../Api/Defaults";

describe("Test account component: SignupForm.", () => 
{
    it("Renders correctly '<SignupForm />' when content is loaded.", () => 
    {
        const tree = shallow(<SignupForm signupForm={signupFormDefault} isLoading={false} />);
        expect(tree).toMatchSnapshot();
    });
});
