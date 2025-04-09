import React from "react";
import { useForm, useFieldArray } from 'react-hook-form';

type FormData = {
    Title: string;
    Description: string;
    Ingredients: Ingredient[];
};

type Ingredient = {
    IngredientName: string;
    Weight: number;
};

const RecipeForm: React.FC = () => {
    const {
        register,
        control,
        handleSubmit,
        formState: { errors },
    } = useForm<FormData>({
        defaultValues: {
            Ingredients: [{ IngredientName: '', Weight: 0 }],
        },
    });

    const { fields, append, remove } = useFieldArray({
        control,
        name: "Ingredients",
    });

    const onSubmit = (data: FormData) => {
        console.log("Dane z formularza:", data);
        // wysłanie do API
    };

    return (
        <form onSubmit={handleSubmit(onSubmit)}>
            <div>
                <label>Nazwa przepisu </label>
                <input
                    type="text"
                    {...register("Title", { required: "Tytuł jest wymagany" })}
                />
                {errors.Title && <p style={{ color: "red" }}>{errors.Title.message}</p>}
            </div>

            <div>
                <label>Opis</label>
                <input
                    type="text"
                    {...register("Description", { required: "Opis jest wymagany" })}
                />
                {errors.Description && (
                    <p style={{ color: "red" }}>{errors.Description.message}</p>
                )}
            </div>

            <div>
                <h3>Składniki</h3>
                {fields.map((field, index) => (
                    <div key={field.id} style={{ marginBottom: "10px" }}>
                        <input
                            placeholder="Nazwa składnika"
                            {...register(`Ingredients.${index}.IngredientName`, {
                                required: "Nazwa składnika jest wymagana",
                            })}
                        />
                        <input
                            type="number"
                            placeholder="Waga (g)"
                            {...register(`Ingredients.${index}.Weight`, {
                                required: "Waga jest wymagana",
                                valueAsNumber: true,
                            })}
                        />
                        <button type="button" onClick={() => remove(index)}>Usuń</button>
                    </div>
                ))}

                <button
                    type="button"
                    onClick={() => append({ IngredientName: "", Weight: 0 })}
                >
                    Dodaj składnik
                </button>
            </div>

            <button type="submit">Zapisz przepis</button>
        </form>
    );
};

export default RecipeForm;
