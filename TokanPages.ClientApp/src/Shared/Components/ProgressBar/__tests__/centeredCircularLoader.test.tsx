import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import CenteredCircularLoader from "../centeredCircularLoader";

describe("Test progress bar component.", () => 
{
    it("Should correctly render 'CenteredCircularLoader' component.", () => 
    {
        const tree = shallow(<CenteredCircularLoader />);
        expect(tree).toMatchSnapshot();
    });
});
