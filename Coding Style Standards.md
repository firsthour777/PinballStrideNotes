




# VARIABLES NAMES

Do not use Underscores.

Never abbreviate variable names, except for situations like i.

Don't put Types in the variable names.

Units should try to be part of names. For example:
delay -> delaySeconds

Do not use -, &, /, ,, _, or anything like that in folder names




# MAKE NEW TYPES

You should create your own Types to be their own variables,
this way when returning or passing values, they will be their own types.

Example:

type UserP = {
 foo: number
}

type Permissions = {
    role: "admin" | "user" | "guest";
    data: UserP | AdminP | GuestP;
}

function getPermissions(role: "admin" | "user" | "guest"): Permissions {
    if (role === "admin"){

    }
}




# ENUMS

Must always be Singular.





# BAD NAMES

Never do names like BaseClass or BaseType. This is sa code smell and should never have heierarchy this way.
You should likely rename the child class.



# BAD CLASSES

Don't use Utils as a class name.





# ABSTRACTION

Abstraction Guidelines:

For simple behaviors and operations do not abstract, even if repeated.

If they are complicated then do abstraction.








# TITLE SUGGESTIONS

It helps to title things as Solution and Project.

Your solution should be called Project,
the project should be called the ProjectProject.

This way it eliminates confusion as to what is the solution and what is the project.

You can also call the Solution, ProjectSolution
and then inside you could see it called Project,
but this might not work as you would have references and such.














# TABLES, DATA STRUCTURES, SQL, JSON, MONGODB, POSTGRESQL

https://stackoverflow.com/questions/218123/what-was-the-strangest-coding-standard-rule-that-you-were-forced-to-follow/218148

Every Table or Data Structure should be named with the following format:

Table Names are all UPPERCASE

prefix between 3 and 6 Characters
PREFIX_TABLENAME

Table names must be Singular.

The First Column in the Table should always be ID and this is the primary key.
Columns should always start with the table's prefix.

Columns must be Singular.

Column names are PascalCase

Column names will be prefixed by teh Table Abrreviation and then an underscore _

Example:

PER_PERSON
    PER_ID
    PER_NameFirst
    PER_NameLast
    ...
CAT_CAT
    CAT_ID
    CAT_Name
    CAT_Breed
    ...
DOG_DOG
    DOG_ID
    DOG_Name
    DOG_Breed
    ...
PERCD_PERSON_CAT_DOG (for the join data)
    PERCD_ID
    PERCD_PER_ID
    PERCD_CAT_ID
    PERCD_DOG_ID




