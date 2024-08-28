import * as React from "react";
import { BusinessFormView } from "./View/businessFormView";

export interface BusinessFormProps {
    pt?: number;
    pb?: number;
    hasCaption?: boolean;
    hasIcon?: boolean;
    hasShadow?: boolean;
    background?: React.CSSProperties;
}

//TODO: add logic
export const BusinessForm = (props: BusinessFormProps): JSX.Element => {
    return(
        <BusinessFormView
            isLoading={false}
            caption="Business Inquiry"
            text=""
            progress={false}
            buttonText="Submit"
            keyHandler={() => {}}
            formHandler={() => {}}
            buttonHandler={() => {}}
            companyText=""
            companyLabel="Company"
            contactText=""
            contactLabel="Contact Name"
            emailText=""
            emailLabel="Email"
            phoneText=""
            phoneLabel="Phone"
            techHeader="Required tech-stack"
            techItems={[
                { value: ".NET/C#", key: 0 },
                { value: "JavaScript/TypeScript", key: 1 },
                { value: "MS SQL/T-SQL", key: 2 },
                { value: "React w/Redux or ContextAPI", key: 3 },
                { value: "Entity Framework/EF Core/Dapper", key: 4 },
                { value: "VPS + Docker images", key: 14 },
                { value: "Azure App Services", key: 5 },
                { value: "Azure Storage", key: 6 },
                { value: "Azure SQL Database", key: 7 },
                { value: "Azure CosmosDb", key: 8 },
                { value: "Azure Key Vault", key: 9 },
                { value: "Azure WebJobs", key: 10 },
                { value: "Azure Functions", key: 11 },
                { value: "Azure Application Insights", key: 12 },
                { value: "Azure Containers", key: 13 }
            ]}
            techLabel=""
            description={{
                frontendText: "",
                frontendLabel: "Frontend description",
                frontendMultiline: true,
                frontendRows: 12,
                backendText: "",
                backendLabel: "Backend description",
                backendMultiline: true,
                backendRows: 12,
                mobileText: "",
                mobileLabel: "Mobile application description",
                mobileMultiline: true,
                mobileRows: 12,
                infoText: "",
                infoLabel: "Additional info",
                infoMultiline: true,
                infoRows: 6
            }}
            pt={props.pt}
            pb={props.pb}
            background={props.background}
            hasIcon={props.hasIcon}
            hasCaption={props.hasCaption}
            hasShadow={props.hasShadow}
        />
    );
};
