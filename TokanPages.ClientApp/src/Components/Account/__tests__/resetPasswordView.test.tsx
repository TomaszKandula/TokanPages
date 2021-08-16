import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import ResetPasswordView from "../resetPasswordView";
import { combinedDefaults } from "../../../Redux/combinedDefaults";

describe("Test account component: ResetForm.", () => 
{
    it("Renders correctly '<ResetPasswordView />' when content is loaded.", () => 
    {
        const tree = shallow(<ResetPasswordView content={combinedDefaults.getResetFormContent.content} isLoading={false} />);
        expect(tree).toMatchSnapshot();
    });
});
