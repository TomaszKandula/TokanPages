import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { UserSignup } from "./userSignup";
import { ApplicationDefault } from "../../../Store/Configuration";

describe("Test account group component: userSignup.", () => 
{
    it("Renders correctly '<UserSignup />' when content is loaded.", () => 
    {
        const tree = shallow(<UserSignup content={ApplicationDefault.contentUserSignup.content} isLoading={false} />);
        expect(tree).toMatchSnapshot();
    });
});