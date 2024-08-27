import * as React from "react";
import { BusinessFormView } from "./View/businessFormView";

//TODO: add logic
export const BusinessForm = (): JSX.Element => {
    return(
        <BusinessFormView
            isLoading={false}
            caption=""
            text=""
            keyHandler={() => {}}
            formHandler={() => {}}
            firstName=""
            lastName=""
            email=""
            subject=""
            message=""
            terms={false}
            buttonHandler={() => {}}
            progress={false}
            buttonText=""
            consent=""
            labelFirstName=""
            labelLastName=""
            labelEmail=""
            labelSubject=""
            labelMessage=""
            multiline={true}
            minRows={6}
        />
    );
};
