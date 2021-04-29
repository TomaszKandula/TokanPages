export interface IFooterContentDto
{
    content: 
    {
        terms: string,
        policy: string,
        copyright: string,
        reserved: string,
        icons:
        {
            firstIcon:
            {
                name: string,
                link: string
            },
            secondIcon:
            {
                name: string,
                link: string
            },
            thirdIcon:
            {
                name: string,
                link: string
            },
        }
    };
}
