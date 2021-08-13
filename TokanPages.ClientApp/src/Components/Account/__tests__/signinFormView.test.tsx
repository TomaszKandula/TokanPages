import "../../../setupTests";
import React from "react";
import { useDispatch } from "react-redux";
import { Dispatch } from "redux";
import { shallow } from "enzyme";
import SigninForm from "../signinForm";
import { combinedDefaults } from "../../../Redux/combinedDefaults";

const jestFunction = jest.fn();
const mockedDispatch = useDispatch as jest.Mock<Dispatch>;
mockedDispatch.mockImplementation(() => jestFunction);

describe("Test account component: SigninForm.", () => 
{
    const wrapper = shallow(<div><SigninForm content={combinedDefaults.getSigninFormContent.content} isLoading={false}/></div>);
    beforeEach(() => wrapper.find("SigninForm").dive());
    
    it("Renders correctly '<SigninForm />' when content is loaded.", () => 
    {
        expect(mockedDispatch).toBeCalledTimes(1);
        expect(wrapper).toMatchSnapshot();
    });
});
