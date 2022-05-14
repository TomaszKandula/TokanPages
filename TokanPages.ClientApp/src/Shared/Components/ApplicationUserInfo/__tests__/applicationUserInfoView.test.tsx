import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { IAuthenticateUserResultDto } from "../../../../Api/Models";
import ApplicationUserInfoView from "../applicationUserInfoView";

describe("Test view component for application user info.", () => 
{
    it("Renders correctly view component with passed props.", () => 
    {
        const testData: IAuthenticateUserResultDto = 
        {
            userId: "5f4d15e2-0d32-4e20-b5f0-5a736152e993",
            email: "happy@tester.com",
            aliasName: "tester",
            avatarName: "tester-avatar-icon.png",
            firstName: "Tester",
            lastName: "Testerovny",
            shortBio: "Happy tester working remotely",
            registered: "2020-01-01 15:02:03",
            userToken: "fbb220c9-7593-472d-bb95-7b41c436e332",
            refreshToken: "5f4d15e2-7593-4e20-b5f0-7b41c436e332",
            roles: 
            [
                {
                    name: "EverydayUser",
                    description: "Standard access"
                },
                {
                    name: "Tester",
                    description: "Can access test environments"
                }
            ],
            permissions: 
            [
                {
                    name: "CanTestEverything"
                },
                {
                    name: "CanOrderPizza"
                }
            ]
        };

        const tree = shallow(<ApplicationUserInfoView bind=
        {{
            state: true,
            data: testData,
            closeHandler: jest.fn()
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
