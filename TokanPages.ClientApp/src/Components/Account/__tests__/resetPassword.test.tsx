import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import ResetPassword from "../resetPassword";
import { combinedDefaults } from "../../../Redux/combinedDefaults";

describe("Test account group component: resetPassword.", () => 
{
    it("Renders correctly '<ResetPassword />' when content is loaded.", () => 
    {
        const tree = shallow(<ResetPassword content={combinedDefaults.getResetPasswordContent.content} isLoading={false} />);
        expect(tree).toMatchSnapshot();
    });
});
