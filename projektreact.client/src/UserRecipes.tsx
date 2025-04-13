import axios from "axios";
import { useEffect, useState } from "react";

type Ingredient = {
    ingredientName: string;
    weight: number;
};

type Recipe = {
    title: string;
    description: string;
    ingredients: Ingredient[];
};


function UserRecipes() {
    const [recipes, setRecipes] = useState<Recipe[]>([]);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchUserRecipes = async () => {
            try {
                const token = localStorage.getItem("token");
                const response = await axios.get('http://localhost:5119/Recipe/GetUserRecipes', {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }

                });

                console.log(response.data);
                setRecipes(response.data);
            } catch (err: any) {
                setError("B³¹d podczas pobierania danych.");
                console.error(err);
            }
        };

        fetchUserRecipes();
    }, []);

    if (error) return <p>{error}</p>;

    return (
        <div>
            <h2>Moje przepisy:</h2>
            {recipes.length === 0 ? (
                <p>Brak przepisów.</p>
            ) : (
                recipes.map((recipe, index) => (
                    <div key={index} style={{ border: "1px solid #ccc", padding: "10px", marginBottom: "10px" }}>
                        <h3>{recipe.title}</h3>
                        <p>{recipe.description}</p>
                        <ul>
                            {recipe.ingredients?.map((ingredient, i) => (
                                <li key={i}>
                                    {ingredient.ingredientName} - {ingredient.weight}g
                                </li>
                            ))}
                        </ul>

                    </div>
                ))
            )}
        </div>
    );
}

export default UserRecipes;
