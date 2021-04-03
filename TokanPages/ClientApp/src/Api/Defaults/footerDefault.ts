import { IFooterContentDto } from "../../Api/Models"

export const footerDefault: IFooterContentDto =
{
    content:
    {
        terms: "",
        policy: "",
        copyright: "",
        reserved: "",
        icons:
        {
            firstIcon:
            {
                name: "",
                link: ""
            },
            secondIcon:
            {
                name: "",
                link: ""
            }
        }
    }    
}
