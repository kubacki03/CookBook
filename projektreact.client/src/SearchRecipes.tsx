import React, { useState } from "react";
import axios from "axios";
import FoundRecipesList from "./FoundRecipesList";

const SearchRecipes: React.FC = () => {
    const [searchTerm, setSearchTerm] = useState("");
    const [recipes, setRecipes] = useState<any[]>([]);

    const handleSearch = async () => {
        if (!searchTerm) return;

        try {
            const response = await axios.get(
                `http://localhost:5119/Recipe/GetRecipes`,
                {
                    params: { name: searchTerm },
                }
            );

            setRecipes(response.data);
        } catch (error) {
            console.error("Error fetching recipes:", error);
        }
    };

    return (
        <div style={{ padding: 20 }}>
            <input
                type="text"
                placeholder="Wpisz nazwę przepisu..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                style={{ padding: 8, fontSize: 16, marginRight: 8 }}
            />
            <button onClick={handleSearch} style={{ padding: 8, fontSize: 16 }}>
                Szukaj
            </button>

            <FoundRecipesList recipes={recipes} />
        </div>
    );
};

export default SearchRecipes;
